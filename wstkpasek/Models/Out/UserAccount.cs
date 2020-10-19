using wstkp.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wstkp.Models.Out
{
  public class UserAccount
  {
    public Profile Profile { get; set; }
    public Settings Settings { get; set; }
    public List<Weight> Weights { get; set; }
  }
}
