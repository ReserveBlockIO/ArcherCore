<p align="center">
  <img src="https://github.com/mathis1337/ArcherCore/assets/20599614/bd62777b-ea9e-4dee-9978-998a7877a445" />
</p>

C# library with a wide variety of tools and utilities in one easy to use location.

[![Generic badge](https://img.shields.io/badge/IDE-VS2022-blue.svg)](https://shields.io/)
[![Generic badge](https://img.shields.io/badge/C%23-10%2E0-blue.svg)](https://shields.io/)
[![Generic badge](https://img.shields.io/badge/%2ENet%20Core-6%2E0-blue.svg)](https://shields.io/)

![license](https://img.shields.io/github/license/mathis1337/ArcherCore)
![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/mathis1337/ArcherCore/dotnet.yml)
![issues](https://img.shields.io/github/issues/mathis1337/ArcherCore)

![GitHub commit activity](https://img.shields.io/github/commit-activity/m/mathis1337/ArcherCore)
![GitHub last commit](https://img.shields.io/github/last-commit/mathis1337/ArcherCore)

![Nuget](https://img.shields.io/nuget/dt/ArcherCore)

Nuget: https://www.nuget.org/packages/ArcherCore

`> dotnet add package ArcherCore --version 1.1.1`

# What is ArcherCore?
ArcherCore is a collection of common tools used in C# (in my opinion) that are simplified and housed in one easy to use library.

#ArcherCore and RBX
This was added to bring many new tools to the Trillium Language. Created by the same developer for RBX.

### ArcherCore Is Built on the Following ###

* C#
* .Net 6.0 | https://dotnet.microsoft.com/en-us/download/dotnet/6.0
* Library
* Visual Studio 2022 | https://visualstudio.microsoft.com/vs/

# Core Features

This library current has utils for the following:
- Compression
- Credit Cards
- CSV Reader
- Dates
- ECDSA
- Email
- Encryption
- Extension Methods
- File IO
- File Download
- Hashing
- Http
- IP
- Logging (Both Text File and DB)
- Memory
- Numbers
- Passwords
- Pinging
- Port Checking
- Randomization
- Strings
- Time
- Timers
- Valid States
- Web Request
- Web Server

# Contributing to ArcherCore
We love your input! We want to make contributing to this project as easy and transparent as possible, whether it's:

- Reporting a bug
- Discussing the current state of the code
- Submitting a fix
- Proposing new features
- Becoming a maintainer

## We Develop with Github
We use github to host code, to track issues and feature requests, as well as accept pull requests.

## We Use [Github Flow](https://guides.github.com/introduction/flow/index.html), So All Code Changes Happen Through Pull Requests
Pull requests are the best way to propose changes to the codebase (we use [Github Flow](https://guides.github.com/introduction/flow/index.html)). We actively welcome your pull requests:

1. Fork the repo and create your branch from `master`.
2. If you've added code that should be tested, add tests.
3. If you've changed APIs, update the documentation.
4. Ensure the test suite passes.
5. Make sure your code lints.
6. Issue that pull request!

## Any contributions you make will be under the MIT Software License
In short, when you submit code changes, your submissions are understood to be under the same [MIT License](http://choosealicense.com/licenses/mit/) that covers the project. Feel free to contact the maintainers if that's a concern.

## Report bugs using Github's [issues](https://github.com/briandk/transcriptase-atom/issues)
We use GitHub issues to track public bugs. Report a bug by [opening a new issue](); it's that easy!

## Write bug reports with detail, background, and sample code
[This is an example](http://stackoverflow.com/q/12488905/180626) of a bug report written, and I think it's not a bad model. Here's [another example from Craig Hockenberry](http://www.openradar.me/11905408), an app developer whom I greatly respect.

**Great Bug Reports** tend to have:

- A quick summary and/or background
- Steps to reproduce
  - Be specific!
  - Give sample code if you can. [My stackoverflow question](http://stackoverflow.com/q/12488905/180626) includes sample code that *anyone* with a base R setup can run to reproduce what I was seeing
- What you expected would happen
- What actually happens
- Notes (possibly including why you think this might be happening, or stuff you tried that didn't work)

People *love* thorough bug reports. I'm not even kidding.

## Use a Consistent Coding Style
I'm again borrowing these from [Facebook's Guidelines](https://github.com/facebook/draft-js/blob/a9316a723f9e918afde44dea68b5f9f39b7d9b00/CONTRIBUTING.md)

* 2 spaces for indentation rather than tabs
* You can try running `npm run lint` for style unification

## License
By contributing, you agree that your contributions will be licensed under its MIT License.

## References
This document was adapted from the open-source contribution guidelines for [Facebook's Draft](https://github.com/facebook/draft-js/blob/a9316a723f9e918afde44dea68b5f9f39b7d9b00/CONTRIBUTING.md)


# How do I get set up?

**Summary of set up**

- For development Visual Studio 2022 is recommended for development. VS Code will also work, but not as much advanced debugging.
- For use setup is simple as binaries are pre-compiled for ease and you can compile them yourself too if desired.

**Configuration**

- Library uses miminal footprint and depending on what you are doing with it will determine your needs. Memory tools are included.

**Dependencies**

- .Net Core 6. Core is available on all platforms (Win, Mac, Linux)
- LiteDB
- Newtonsoft.Json

# Who do I talk to? ###

* Repo owner or admin
* Other community or team contact

# License

ArcherCore is released under the terms of the MIT license. See [COPYING](COPYING) for more
information or see https://opensource.org/licenses/MIT.

### References, Libraries, and Thank you's ###
This code was compiled from 12+ years of projects and random things. If you notice it is similar to something of yours please let me know and I will happily give you credit.

* LiteDB 
* Newtonsoft.Json 
* Starkbank for ECDSA Library
* Brian A. Danielak for Contribution Guidelines
