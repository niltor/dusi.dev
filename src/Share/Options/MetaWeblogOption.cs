using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Options;
public class MetaWeblogOption
{
    public required string BlogName { get; set; }
    public required string Username { get; set; }
    public required string PAT { get; set; }
}
