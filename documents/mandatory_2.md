# Mandatory II - Ali and Brian


**Reflect on how your group uses version control**

**How are you DevOps?**

**Software Quality**

**Security Assessment**

**Monitoring Realization**

---

## Reflect on how your group uses version control

In Team AB, we have used `git` as our version control system. Git gives us a number of benefits such as developing features asynchronous in seperate `branch`'es and provides us with an overview of previous `commits`. In addition, we have used git to edit existing commits using `git rebase -i` to change the commit content or message, squash a commit with another, or remove it entirely. The `git rebase -i` command gives us a unique opportunity to establish a straight line through all our commits from beginning to the end.

To make our commits as transparent as possible, we have followed best practices within git version control. The following are prefixes that we have added in our commit messages, which vary depending on the specific commit's content:

- `feat` is for adding a new feature
- `fix` is for fixing a bug
- `refactor` is for changing code for peformance or convenience purpose (e.g. readibility)
- `chore` is for everything else (writing documentation, formatting, adding tests, cleaning unused code etc.)

Despite `git`'s advantages, it is nevertheless fallible developers who have to comply with the rules that we in the group have set for ourselves. And during development phases with extensive pressure, we have been bad at maintaining our commit discipline, which is why commits with questionable messages can regularly be discovered in our commit history in the `main` branch.

## How are you DevOps?

## Software Quality

We have used Code Climate and SonarQube as our software quality tools to reduce smelly code, vulnerabilities in our security and eliminate repetitive code.

We agreed largely with the suggested improvements of the tools we used. This applies to repetitive code where components could have been used instead. This is especially the case with the Login and Sign-up form, where components such as `<TextInput/>` containing a `label` and `input` were to be developed instead.

Cases where we have chosen to ignore or reject the tools' instructions have been updating dependencies in the legacy codebase, `./whoknows_variations`, and our database migrations in `./backend/api/Migrations` as well as seeding data in `./backend/api/Data` as SonarQube was insisting on removing duplicates and repetitive code which were false-positive.

## Security Assessment

## Monitoring Realization

Having set up monitoring of our API and Linux server using Prometheus and Grafana, we noticed that the `GET /api/weather' endpoint, which communicates with an external API, had a predominantly high response time which could reach up to 10 seconds(!).

We came to the conclusion that this must be rectified. An 'issue' on GitHub has therefore been created to find a faster external weather API that can meet our requirements for fast response times.

Why are fast response times important? [Amazon found out](https://www.gigaspaces.com/blog/amazon-found-every-100ms-of-latency-cost-them-1-in-sales/) that every 100ms of latency cost them 1% in sales. With that in mind, we set ourselves to fix the above-mentioned issue.

### Made by Ali & Brian, 26/11/2024
