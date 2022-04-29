using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;

namespace NetworkMonitor.Services
{
    internal class SortableBindingList<T> : BindingList<T>
    {
        internal SortableBindingList(IList<T> list) : base(list) { }
        public SortableBindingList() : base() { }

        private bool isSorted = false;
        private PropertyDescriptor sortProperty = null;
        private ListSortDirection sortDirection = ListSortDirection.Ascending;

        protected override void RemoveSortCore()
        {
            isSorted = false;
            sortProperty = null;
        }

        protected override ListSortDirection SortDirectionCore => sortDirection;
        protected override PropertyDescriptor SortPropertyCore => sortProperty;
        protected override bool IsSortedCore => isSorted;
        protected override bool SupportsSortingCore => true;

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            var propertyType = prop.PropertyType;

            if (PropertyComparer.IsAllowable(propertyType))
            {
                ((List<T>)Items).Sort(new PropertyComparer(prop, direction));
                sortDirection = direction;
                sortProperty = prop;
                isSorted = true;
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }

        internal class PropertyComparer : Comparer<T>
        {
            private readonly PropertyDescriptor prop;
            private readonly IComparer comparer;
            private readonly ListSortDirection direction;
            private readonly bool useToString;

            internal PropertyComparer(PropertyDescriptor prop, ListSortDirection direction)
            {
                if (prop.ComponentType != typeof(T))
                {
                    throw new MissingMemberException(typeof(T).Name, prop.Name);
                }

                this.prop = prop;
                this.direction = direction;

                if (OkWithIComparable(prop.PropertyType))
                {
                    var comparerType = typeof(Comparer<>).MakeGenericType(prop.PropertyType);
                    var defaultComparer = comparerType.GetProperty("Default");
                    comparer = (IComparer)defaultComparer.GetValue(null, null);
                    useToString = false;
                }
                else if (OkWithToString(prop.PropertyType))
                {
                    comparer = StringComparer.CurrentCultureIgnoreCase;
                    useToString = true;
                }
            }

            public override int Compare(T x, T y)
            {
                var xValue = prop.GetValue(x);
                var yValue = prop.GetValue(y);

                if (useToString)
                {
                    xValue = xValue?.ToString();
                    yValue = yValue?.ToString();
                }

                return direction == ListSortDirection.Ascending
                    ? comparer.Compare(xValue, yValue)
                    : comparer.Compare(yValue, xValue);
            }

            protected static bool OkWithToString(Type t)
            {
                return (t.Equals(typeof(XNode)) || t.IsSubclassOf(typeof(XNode)));
            }

            protected static bool OkWithIComparable(Type t)
            {
                return (t.GetInterface("IComparable") != null)
                    || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
            }

            public static bool IsAllowable(Type t)
            {
                return OkWithToString(t) || OkWithIComparable(t);
            }
        }
    }
}
