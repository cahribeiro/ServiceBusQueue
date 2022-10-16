<h1 align="center">
	<!-- <img alt="Logo" src=".github/logo.png" width="200px" /> -->
  .NET Console Application to connect with Azure ServiceBus Queue
</h1>

<p align="center">

  <a href="https://www.linkedin.com/in/ana-carolina-ribeiro-santos/">
    <img alt="Made by" src="https://img.shields.io/badge/made%20by-Carolina%20Ribeiro-blue">
  </a>
  
  <img alt="GitHub" src="https://img.shields.io/badge/license-MIT-green">
</p>

## 👩🏻‍💻 About the project

- <p style="color: blue;">Console Application that connects to Azure ServiceBus Queue, send messages to queue and receive messages from the queue.</p>

## 💻 Getting started

**Clone the project and access the folder**

```bash
$ git clone https://github.com/cahribeiro/ServiceBusQueue.git && cd ServiceBusQueue
```

**Follow the steps below**

```bash
# Configuration in Azure
create an Azure ServiceBus Namespace
create a Queue in serviceBus
inside the Queue, click on "Shared Access Policies" and Add a new policy to Send and Listen.

#Configuration in project
Copy the Primary Connection String from "Shared Access Policies" and paste inside the code string "conn".
Change the string value "sbName" to your own Queue name.

```

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

Made with 💜 &nbsp;by Carolina Ribeiro 👋 &nbsp;[See my linkedin](https://www.linkedin.com/in/ana-carolina-ribeiro-santos/)
