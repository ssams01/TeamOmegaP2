using System;
using System.Collections.Generic;

namespace DatabaseFirstEx.Model;

public partial class Place
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int Language { get; set; }

    public int BiomType { get; set; }
}
