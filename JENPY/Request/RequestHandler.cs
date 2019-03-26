using System;
namespace JENPY.Request
{
public interface RequestHandler
    {
    JenpyObject Handle(JenpyObject req);
    }
}
