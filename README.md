# README #

### What is this repository for? ###

Web app to count posted words and calculate stats in real time. It's a 3-tier application - API, Domain and Infrastructure. Infrastructure is responsible for DB access, Domain has a business logic and API layer makes sure that client will not get an http 500 error. 

Projects linked by Autofac modules. Also, there are 2 more projects with tests - Backend.Domain.Tests which has examples of business logic tests with mocks and Backend.StressTests which has an end-to-end test (it sends 100 requests with total 15700 words in up to 20 threads simultaneously).