// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable once CheckNamespace
namespace Microsoft.Data.Entity.Metadata
{
    public static class EntityTypeExtensions
    {
        [CanBeNull]
        public static IProperty TryGetPropertyByStorageName([NotNull] this IEntityType entityType, [NotNull] string name)
        {
            Check.NotNull(entityType, "entityType");
            Check.NotEmpty(name, "name");
            return entityType.Properties.FirstOrDefault(s => s.StorageName == name);
        }

        [NotNull]
        public static IProperty GetPropertyByStorageName([NotNull] this IEntityType entityType, [NotNull] string name)
        {
            Check.NotNull(entityType, "entityType");
            Check.NotEmpty(name, "name");

            var property = TryGetPropertyByStorageName(entityType, name);
            if (property == null)
            {
                throw new ModelItemNotFoundException(Strings.FormatPropertyWithStorageNameNotFound(name, entityType.Name));
            }
            return property;
        }

        [NotNull]
        public static IEnumerable<IForeignKey> GetReferencingForeignKeys([NotNull] this IEntityType entityType)
        {
            return entityType.Model.GetReferencingForeignKeys(entityType);
        }
    }
}
