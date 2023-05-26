
select 
DB_NAME() as 'účetní jednotka',
fapol.stext,
ISNULL(fapol.pozn,'-') as 'Položka pozn',
ISNULL(fapol.VCislo,'-') as 'Výrobní číslo',

case 
WHEN fa.RelTpFak = 11 THEN 'Faktury přijaté'
WHEN fa.RelTpFak = 1 THEN 'Faktury vydané'
ELSE 'nevím' END as 'Typ dokladu',

fa.cislo,
ISNULL(fa.Firma,'-'),
fa.Datum,
ISNULL(fa.Pozn,'-') as 'FA pozn',
ISNULL(fa.pozn2,'-') as 'FA pozn 2'

from fapol
left join fa on fapol.refag = fa.id

WHERE FA.RelTpFak IN (1,11)
AND
fapol.stext like '%' + @hledanyVyraz + '%'
OR fapol.pozn like '%' + @hledanyVyraz + '%'
or fapol.VCislo like '%' + @hledanyVyraz + '%'
or fa.Pozn like '%' + @hledanyVyraz + '%'
or fa.pozn2 like '%' + @hledanyVyraz + '%'
