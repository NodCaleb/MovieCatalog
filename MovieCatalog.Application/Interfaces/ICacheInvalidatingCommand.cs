using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalog.Application.Interfaces;

/// <summary>
/// Represents a command that should invalidate a cache entry
/// </summary>
public interface ICacheInvalidatingCommand
{
	string CacheKey { get; }
}
