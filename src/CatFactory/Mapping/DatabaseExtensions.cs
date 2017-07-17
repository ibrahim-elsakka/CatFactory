﻿using System;
using System.Linq;

namespace CatFactory.Mapping
{
    public static class DatabaseExtensions
    {
        public static void AddPrimaryKeyToTables(this Database database)
        {
            foreach (var table in database.Tables)
            {
                if (table.PrimaryKey == null && table.Columns.Count > 0)
                {
                    table.PrimaryKey = new PrimaryKey(table.Columns[0].Name);
                }
            }
        }

        public static void SetIdentityForTables(this Database database, params String[] exclusions)
        {
            foreach (var table in database.Tables)
            {
                if (table.Identity == null && table.Columns.Count > 0)
                {
                    table.Identity = new Identity(table.Columns[0].Name, 1, 1);
                }
            }
        }

        public static void AddColumnsForTables(this Database database, Column[] columns, params String[] exclusions)
        {
            foreach (var table in database.Tables)
            {
                if (exclusions != null && exclusions.Contains(table.FullName))
                {
                    continue;
                }

                foreach (var column in columns)
                {
                    if (!table.Columns.Contains(column))
                    {
                        table.Columns.Add(column);
                    }
                }
            }
        }

        public static void AddColumnForTables(this Database database, Column column, params String[] exclusions)
        {
            AddColumnsForTables(database, new Column[] { column }, exclusions);
        }

        public static void LinkTables(this Database db)
        {
            foreach (var table in db.Tables)
            {
                foreach (var column in table.Columns)
                {
                    if (table.PrimaryKey != null && table.PrimaryKey.Key.Count == 1 && table.PrimaryKey.Key.Contains(column.Name))
                    {
                        continue;
                    }

                    foreach (var parentTable in db.Tables)
                    {
                        if (table.FullName == parentTable.FullName)
                        {
                            continue;
                        }

                        if (parentTable.PrimaryKey != null && parentTable.PrimaryKey.Key.Contains(column.Name))
                        {
                            table.ForeignKeys.Add(new ForeignKey(column.Name)
                            {
                                ConstraintName = db.NamingConvention.GetForeignKeyConstraintName(table.Name, new String[] { column.Name }, parentTable.Name),
                                References = String.Format("{0}.{1}", db.Name, parentTable.FullName),
                                Child = table.FullName
                            });

                        }
                    }
                }
            }
        }

        public static void AddRelation(this Database db, String source, String[] sourceKey, String target)
        {
            // todo: Add logic for this operation
        }

        public static void AddRelation(this Database db, String source, String sourceKey, String target)
        {
            AddRelation(db, source, new String[] { sourceKey }, target);
        }
    }
}
