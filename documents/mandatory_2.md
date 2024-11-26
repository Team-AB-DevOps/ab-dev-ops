# Mandatory II - Ali and Brian

**Reflect on how your group uses version control**

**How are you DevOps?**

**Software Quality**

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

In Team AB we have strived for being DevOps since the beginning of the course. Generally speaking, DevOps is a specific culture within a developing team, but Devops also includes a set of technical practices, which we will discuss.

An integral part of DevOps technical practices is Continuous Integration/Delivery and automation. In Team AB we have implemented both practices to deliver our product to the costumer quickly and reliably using GitHub Actions which supplied us with automated pipelines. Every push to a branch triggers a workflow, which builds and tests the code. We then manually decide when to push the main branch to production, by creating a new tag release, which triggers our deployment workflow.

Another technical practice is continuous improvement. To be able to do so, we have utilized smokescreen tests running regularly each hour, to ping our API endpoints to check if changes can be made to improve our system to meet demand. On top of that, we have set up a monitoring system using Prometheus with Grafana, which gives us an overall view of the system utilization of the hosted server and http response statuses for each indiviual endpoint in our system. In doing so, we can quickly recognize when a specific endpoint or our whole API is failing or not being able to meet demand.

In regards of the cultural practices, safety thinking [psykologisk sikkerhed] is paramount. In Team AB, safety thinking was an easy and natural thing to implement. Discussions and arguments could easily take place without fear of 'losing face' or being criticized. This is largely due to Team AB consisting of two developers, which have known eachother since the beginning of our computer science degree.

In Team AB we also have some shortcomings, where we were not being DevOps, such as lack of resilience engineering, swarming and infrastructure as code.

We have not focused on designing a system that can easily recover and continue functioning during failures, aka. Resilience Engineering. We lack backups of important parts of the system (such as the database), so in case of a critical failure of the cloud container, there is no way to quickly spin up a new one with the data intact. To improve this, we would have to take regular backups of the data, and keep them at seperate locations, so if one fails, it can easily be recovered. This would require us to spend additional money, which is above the scope of this elective and beyond our pockets.

Currently, our deployed services have been manually created and maintained. This may cause several issue should our deployment fail and re-deployment is required. Doing so manually would take us several days to get everything back as before, which generally speaking is fatal for our costumer. Infrastructure as code would solve this issue and be able to deploy our systems in no time resulting in a short downtime for our customer. Today, what keeps us from transfering to infrastrucure as code is the amount of time it would take. Today, our deployment server takes shape as a highly customized 'pet' instead of a generic 'cattle', which is easily replaceable. Remaking our deployment server to fit the latter definition is the first step towards automating and generalizing our infrastrucure as code should we decide to do so in later iterations.    

Due to different schedules, it was occasionally not possible to help each other with tasks that were causing issues (aka. swarming). This would result in Team AB having to solve issues individually, which meant it might have taken longer than if it had been swarmed. This is an obvious disadvantage of being a two-person team, which can only be solved by being a larger development team.

## Software Quality

We have used Code Climate and SonarQube as our software quality tools to reduce smelly code, vulnerabilities in our security and eliminate repetitive code.

We agreed largely with the suggested improvements of the tools we used. This applies to repetitive code where components could have been used instead. This is especially the case with the Login and Sign-up form, where components such as `<TextInput/>` containing a `label` and `input` were to be developed instead.

Cases where we have chosen to ignore or reject the tools' instructions have been updating dependencies in the legacy codebase, `./whoknows_variations`, and our database migrations in `./backend/api/Migrations` as well as seeding data in `./backend/api/Data` as SonarQube was insisting on removing duplicates and repetitive code which were false-positive.

## Monitoring Realization

Having set up monitoring of our API and Linux server using Prometheus and Grafana, we noticed that the `GET /api/weather' endpoint, which communicates with an external API, had a predominantly high response time which could reach up to 10 seconds(!).

We came to the conclusion that this must be rectified. An 'issue' on GitHub has therefore been created to find a faster external weather API that can meet our requirements for fast response times.

Why are fast response times important? [Amazon found out](https://www.gigaspaces.com/blog/amazon-found-every-100ms-of-latency-cost-them-1-in-sales/) that every 100ms of latency cost them 1% in sales. With that in mind, we set ourselves to fix the above-mentioned issue.

### Made by Ali & Brian, 26/11/2024
