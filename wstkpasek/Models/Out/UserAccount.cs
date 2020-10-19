using wstkpasek.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wstkpasek.Models.Out
{
  public class UserAccount
  {
    public Profile Profile { get; set; }
    public Settings Settings { get; set; }
    public List<Weight> Weights { get; set; }
  }
}
