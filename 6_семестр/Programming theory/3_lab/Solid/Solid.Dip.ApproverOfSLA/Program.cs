using Solid.Dip.Personnel;

var minTimeOffPercent = 98;
var maxResolutionDays = 2;
var companySla = new ServiceLicenseAgrement(minTimeOffPercent, maxResolutionDays);

var subcontractor1 = new Subcontractor("Rebekah Jackson");
var subcontractor2 = new Subcontractor("Harry Fitz");
var collaborators = new Subcontractor[] { subcontractor1, subcontractor2 };

foreach (var subcontractor in collaborators)
    subcontractor.ApproveSLA(companySla);

Console.ReadKey();