﻿namespace TheWayToGerman.Core.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; } = DateTime.Now;
    public DateTime DeleteDate { get; set; }
}