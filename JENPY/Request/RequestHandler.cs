using System;
namespace JENPY.Request
{
    public interface RequestHandler
    {
        JenpyResponse Handle(string verb);
    }
}
