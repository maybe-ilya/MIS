using System;

public static class BootstrapServices
{
    private readonly static Type[] _servicesTypes = new Type[0];

    public static Type[] ServicesTypes => _servicesTypes;
}