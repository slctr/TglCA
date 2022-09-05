﻿using TglCA.Dal.Interfaces.Entities.Base;

namespace TglCA.Dal.Interfaces.Entities;

public class Currency : BaseEntity
{
    public string CurrencyId { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public byte[] Img { get; set; }
    public bool IsDeleted { get; set; } = false;
}