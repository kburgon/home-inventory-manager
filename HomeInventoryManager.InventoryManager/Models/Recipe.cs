namespace HomeInventoryManager.Shared;

/// <summary>
/// Represents a CookCLI Recipe JSON object.
/// </summary>
public class Recipe
{
	public List<Cookware> Cookwares { get; set; } = new();
}

/// <summary>
/// A piece of cookware listed in a CookCLI recipe.
/// </summary>
public class Cookware
{
	public string Name { get; set; } = string.Empty;
}

/// <summary>
///	A step in a CookCLI recipe.
/// </summary>
public class Step
{
	public string Description { get; set; } = string.Empty;
}

/// <summary>
///	An ingredient in a CookCLI recipe.
/// </summary>
public class Ingredient
{
	public string Name { get; set; } = string.Empty;
	
	public string Amount { get; set; } = string.Empty;
}

/// <summary>
///	A piece of metadata in a CookCLI recipe.
/// </summary>
public class Metadata
{
}
