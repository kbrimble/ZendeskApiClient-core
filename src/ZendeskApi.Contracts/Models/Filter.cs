﻿namespace ZendeskApi.Contracts.Models
{
    public struct Filter
    {
        public string Field;
        public string Value;
        public FilterOperator FilterOperator;
    }
}