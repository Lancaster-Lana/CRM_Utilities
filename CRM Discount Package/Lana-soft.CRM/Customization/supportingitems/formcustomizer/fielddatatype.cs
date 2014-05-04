using System.ComponentModel;

namespace LanaSoftCRM
{
	public enum FieldDataType
	{
		[DescriptionAttribute("{67FAC785-CD58-4f9f-ABB3-4B7DDC6ED5ED}")] BooleanRadio,
		[DescriptionAttribute("{B0C6723A-8503-4fd7-BB28-C8A06AC933C2}")] BooleanCheckbox,
		[DescriptionAttribute("{5B773807-9FB2-42db-97C3-7A91EFF8ADFF}")] DateTime,
		[DescriptionAttribute("{C3EFE0C3-0EC6-42be-8349-CBD9079DFD8E}")] Decimal,
		[DescriptionAttribute("{AA987274-CE4E-4271-A803-66164311A958}")] Duration,
		[DescriptionAttribute("{ADA2203E-B4CD-49be-9DDF-234642B43B52}")] Email,
		[DescriptionAttribute("{0D2C745A-E5A8-4c8f-BA63-C6D3BB604660}")] Float,
		[DescriptionAttribute("{FD2A7985-3187-444e-908D-6624B21F69C0}")] IFrame,
		[DescriptionAttribute("{C6D124CA-7EDA-4a60-AEA9-7FB8D318B68F}")] Integer,
		[DescriptionAttribute("{270BD3DB-D9AF-4782-9025-509E298DEC0A}")] Lookup,
		[DescriptionAttribute("{E0DECE4B-6FC8-4a8f-A065-082708572369}")] Memo,
		[DescriptionAttribute("{533B9E00-756B-4312-95A0-DC888637AC78}")] Money,
		[DescriptionAttribute("{CBFB742C-14E7-4a17-96BB-1A13F7F64AA2}")] PartyList,
		[DescriptionAttribute("{3EF39988-22BB-4f0b-BBBE-64B5A3748AEE}")] Picklist,
		[DescriptionAttribute("{F3015350-44A2-4aa0-97B5-00166532B5E9}")] Regarding,
		[DescriptionAttribute("{5D68B988-0661-4db2-BC3E-17598AD3BE6C}")] Status,
		[DescriptionAttribute("{4273EDBD-AC1D-40d3-9FB2-095C621B552D}")] Text,
		[DescriptionAttribute("{E0DECE4B-6FC8-4a8f-A065-082708572369}")] TextArea,
		[DescriptionAttribute("{1E1FC551-F7A8-43af-AC34-A8DC35C7B6D4}")] TickerSymbol,
		[DescriptionAttribute("{7C624A0B-F59E-493d-9583-638D34759266}")] TimeZone,
		[DescriptionAttribute("{71716B6C-711E-476c-8AB8-5D11542BFB47}")] Url
	}
}