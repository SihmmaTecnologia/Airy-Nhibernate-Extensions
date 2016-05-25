﻿using System;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using NodaTime;

namespace Dematt.Airy.Nhibernate.NodaTime
{
    public class DateTimeZoneTzdbType : IUserType
    {
        public SqlType[] SqlTypes
        {
            get { return new SqlType[] { SqlTypeFactory.GetString(35) }; }
        }

        public Type ReturnedType
        {
            get { return typeof(DateTimeZone); }
        }

        public bool IsMutable
        {
            get { return false; }
        }

        public new bool Equals(object x, object y)
        {
            return object.Equals(x, y);
        }

        public int GetHashCode(object x)
        {
            return x == null ? 0 : x.GetHashCode();
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            var value = NHibernateUtil.String.NullSafeGet(rs, names);
            if (value == null)
            {
                return null;
            }
            return DateTimeZoneProviders.Tzdb.GetZoneOrNull((string)value);
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            if (value == null)
            {
                NHibernateUtil.String.NullSafeSet(cmd, null, index);
            }
            else
            {
                NHibernateUtil.String.NullSafeSet(cmd, ((DateTimeZone)value).Id, index);
            }
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        public object Disassemble(object value)
        {
            return value;
        }
    }
}
