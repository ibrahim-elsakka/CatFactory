﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using CatFactory.Mapping;

namespace CatFactory
{
    [DebuggerDisplay("Name={Name}, DbObjects={DbObjects.Count}")]
    public class ProjectFeature
    {
        public ProjectFeature()
        {
        }

        public ProjectFeature(String name, IEnumerable<DbObject> dbObjects, Database database)
        {
            Name = name;
            DbObjects.AddRange(dbObjects);
            Database = database;
        }

        public String Name { get; set; }

        public String Description { get; set; }

        public Database Database { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<DbObject> m_dbObjects;

        public List<DbObject> DbObjects
        {
            get
            {
                return m_dbObjects ?? (m_dbObjects = new List<DbObject>());
            }
            set
            {
                m_dbObjects = value;
            }
        }
    }
}
