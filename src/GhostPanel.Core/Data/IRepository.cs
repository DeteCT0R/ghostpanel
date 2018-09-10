﻿using System.Collections.Generic;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;

namespace GhostPanel.Core.Data
{
    public interface IRepository
    {
        T Single<T>(ISpecification<T> spec) where T : DataEntity;
        List<T> List<T>(ISpecification<T> spec = null) where T : DataEntity;
        T Create<T>(T dataItem) where T : DataEntity;
        T Update<T>(T dataItem) where T : DataEntity;
        void Update<T>(List<T> dataItemList) where T : DataEntity;
        void Create<T>(List<T> dataItemList) where T : DataEntity;
        void Remove<T>(T dataItem) where T : DataEntity;
    }
}
