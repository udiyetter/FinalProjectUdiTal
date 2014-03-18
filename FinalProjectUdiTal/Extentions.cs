using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProjectUdiTal
{
    internal static class Extentions
    {
        internal static string RepresentativeStringOf(this Student.ePreferences self)
        {
            return StudentPreference.PreferencesMapping.ContainsKey(self) ?
                StudentPreference.PreferencesMapping[self] :
                StudentPreference.PreferencesMapping[Student.ePreferences.noPreference];
        }
    }
}
