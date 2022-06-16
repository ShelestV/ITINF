var minTimeOffPercent = 98;
var maxResolutionDays = 2;
var companySla = new Solid.Dip.Personnel.ServiceLicenseAgrement(minTimeOffPercent, maxResolutionDays);

var subcontractor1 = new Solid.Dip.Personnel.Subcontractor("Rebekah Jackson");
var subcontractor2 = new Solid.Dip.Personnel.Subcontractor("Harry Fitz");
var collaborators = new Solid.Dip.Personnel.Subcontractor[] { subcontractor1, subcontractor2 };

foreach (var subcontractor in collaborators)
    subcontractor.ApproveSLA(companySla);