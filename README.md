# README #

-- pl

GGPROJ jest symulatorem zakupu i sprzedaży towarów, w tym konkretnym przypadku przygotowany pod sprzedaż owoców i warzyw. Symuluje wpisy w książce przychodów i rozchodów która musi być prowadzona przez małych sprzedawców nie korzystających z kas fiskalnych. 

Wpisy przychodów są generowane na podstawie zaznaczonej opcji 'Kup', natomiast wpisy rozchodów generowane są losowo, symulując 'podejścia' klientów do momentu zejścia danego towaru ze stanu.

W pliku lista.xml znajduje się przykładowa baza z towarami na których można dokonać symulacji.


-- eng

GGProj is simulator of sale and purchase of goods, in this case prepared for vegetables and fruits sale. It generate entries in receipts and expenses book, which small entrepreneurs have to keep if they don't use cash register.

Purchase entries are generated when 'Buy?' checkbox is selected. However, sale entries are simulating purchases, so they are generated randomly, until stocks of goods are out.

In the included file 'lista.xml' you can find xml-database with example list of goods. Feel free to import and use it with simulation in the program.