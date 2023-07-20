using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argutec.EfCore.Revisions
{
    public enum RevisionOperation
    {
        Insert = 0,
        Update = 10
    }
}