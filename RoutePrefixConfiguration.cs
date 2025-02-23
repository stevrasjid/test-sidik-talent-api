using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

public class RoutePrefixConfiguration : IApplicationModelConvention
{
    private readonly string _prefix;
    public RoutePrefixConfiguration(string prefix)
    {
        _prefix = prefix;
    }

    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            var routeAttribute = controller.Attributes.OfType<RouteAttribute>().FirstOrDefault();
            if (routeAttribute == null)
            {
                controller.Selectors[0].AttributeRouteModel = new AttributeRouteModel
                {
                    Template = $"{_prefix}/{controller.ControllerName.ToLower()}"
                };
            }
        }
    }
}