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

## Security Assessment

## Monitoring Realization

I forlængelse af at have opsat monitorering af vores API og Linux server bed vi mærke til, at `GET /api/weather` endpoint, som kommunikerer med et eksternt API, havde en overvejende høj responstid op til 10 sekunder(!). 

Dette har fået os til at kigge hinanden dybt i øjnene, hvorefter vi er kommet frem til, at dette må udbedres. Et 'issue' på GitHub er derfor blevet oprettet mhp. at finde en bedre ekstern weather API, der kan tilgodese vores behov for hurtig responstid.

Hvorfor er hurtig responstid vigtig? [Amazon har udarbejdet en undersøgelse](https://www.gigaspaces.com/blog/amazon-found-every-100ms-of-latency-cost-them-1-in-sales/), der konkluderer, at hver 100ms responstid koster dem 1 pct. i salg. Vores beslutning skal ses med dette in mente.

### Made by Ali & Brian, 17/11/2024
