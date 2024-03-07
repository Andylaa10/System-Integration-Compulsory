﻿namespace UserService.Core.Entities.Helper;

public class PaginatedResult<T>
{
    public List<T> Items { get; set; }
    public int TotalCount { get; set; }
}