using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.ERP.Models.ACC
{
  public class FileUploads : BaseClass
  {
    public Guid Id { get; set; }
    public Guid OrganisationId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public float Amount { get; set; }
    public string FileName { get; set; }
    public string Status { get; set; }
    public string UploadType { get; set; }
  }
}
