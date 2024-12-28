using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalog.Application.Interfaces;

public interface ICacheInvalidatingCommand
{
	string CacheKey { get; }
}
