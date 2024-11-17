# Mandatory II - Ali and Brian


**Reflect on how your group uses version control**

**How are you DevOps?**

**Software Quality**

**Security Assessment**

**Monitoring Realization**

---

## Reflect on how your group uses version control

## How are you DevOps?

## Software Quality

Vi har anvendt Code Climate og SonarQube som vores software quality værktøjer mhp. at reducere smelly code, sårbarheder i vores sikkerhed og repetetiv kode.

Vi var i overvejende høj grad enig med vores anvendte værktøjers forslag til forbedringer. Dette gælder repetetiv kode, hvor der ikke er blevet anvendt komponenter. Det er især tilfældet ved Login og Sign-up formularen, hvor komponenter såsom `<TextInput/>` indeholdende et `label` og `input`.

Tilfælde, hvor vi har valgt at ignorere eller afvise værktøjernes anvisninger, har været opdatering af dependencies i legacy-kodebasen, `./whoknows_variations`, og vores database migrations i `./backend/api/Migrations` samt seeding data i `./backend/api/Data`, da SonarQube var insisteren på at fjerne falsk-positive duplikater.

## Security Assessment

## Monitoring Realization

I forlængelse af at have opsat monitorering af vores API og Linux server vha. Prometheus og Grafana bed vi mærke til, at `GET /api/weather` endpoint, som kommunikerer med et eksternt API, havde en overvejende høj responstid op til 10 sekunder(!).

Dette har fået os til at kigge hinanden dybt i øjnene, hvorefter vi er kommet frem til, at dette må udbedres. Et 'issue' på GitHub er derfor blevet oprettet mhp. at finde en bedre ekstern weather API, der kan tilgodese vores behov for hurtig responstid.

Hvorfor er hurtig responstid vigtig? [Amazon har udarbejdet en undersøgelse](https://www.gigaspaces.com/blog/amazon-found-every-100ms-of-latency-cost-them-1-in-sales/), der konkluderer, at hver 100ms responstid koster dem 1 pct. i salg. Vores beslutning skal ses med dette in mente.

### Made by Ali & Brian, 17/11/2024
