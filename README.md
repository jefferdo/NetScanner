Table of Contents {#table-of-contents .TOC-Heading}
=================

[Aims 2](#aims)

[Description 2](#description)

[Background research 2](#background-research)

[Tool Implementation 3](#tool-implementation)

[Technologies 3](#technologies)

[Class Diagram 4](#class-diagram)

[Screens 5](#screens)

[Main Menu 5](#main-menu)

[Interfaces 5](#interfaces)

[Scan 7](#scan)

[Snapshot history 9](#snapshot-history)

[Challengers 11](#challengers)

[Other Similar tools 12](#other-similar-tools)

[IP Address Tracker 12](#ip-address-tracker)

[Angry IP Scanner (Free) 12](#angry-ip-scanner-free)

[NETDISCOVER 12](#netdiscover)

Aims
====

-   Scan Network for connected devices

-   Save scan results

-   View or compare saved logs

Description
===========

It is always better to be aware about the network which we are connected
and about other devices which are connected to the same network. "Man,
in the middle" attack is one of common attack vectors nowadays in public
networks. To be aware about the network I have prepared a small tool to
scan a network and obtain device NIC information. The main Features are,

-   Select network interface card from the list and start scan. Ip range
    will be automatically calculated using the IP information.

-   Save scan results into files

-   View or compare saved logs.

Background research
===================

To scan a network, we mainly need two points of information.

1.  Network ID -- "is the portion of an IP address that identifies the
    TCP/IP network on which a host resides. The network ID portion of an
    IP address uniquely identifies the host\'s network on an
    internetwork, while the host ID portion of the IP address identifies
    the host within its network." Also, the first address of the IP
    range

2.  Broadcast ID -- "A broadcast address is a network address at which
    all devices connected to a multiple-access communications network
    are enabled to receive datagrams, which comprise UDP and TCP/IP
    packets, for instance. A message sent to a broadcast address may be
    received by all network-attached hosts." Also, the last address of
    the IP range

With this information we can calculate available host range within the
connected network. Network ID can be calculated using bitwise AND
operation between any random IP address and the subnet mask. Example,

![](.//media/image1.png){width="6.268055555555556in" height="1.00625in"}

To obtain the broadcast address we have to perform bitwise OR opration
between Network ID and Inversed Subnetmask. Example,

![](.//media/image2.png){width="6.268055555555556in"
height="1.0166666666666666in"}

With this information we can obtain the host range of the network.

To find the active device of a network, we can use two methods,

-   Address Resolution Protocol -- containing the IP addresses, MAC
    addresses, and allocation type

-   Ping (ICMP Protocol) -- This will enable you to further narrow down
    what devices are active

But depends on the network implementation and firewall rules the
efficiency of the above methods may vary.

The tools main finality will enhance these methods to obtain active
device infections.

Tool Implementation 
===================

Technologies
------------

During the initial research I have identified many languages which can
perform required operations to obtain information. Top selections were
Python3 and .net Framework. But we chose to .net framework for the
flexibility of windows form implementations. But I had to sacrifice the
amount of code lines which we could use if we went with Python3 which is
less.

  .Net Framework (C\#)                         Python3
  -------------------------------------------- --------------------------------
  Windows Only (Newer Versions)                Platform Independent
  Minimum configurations on deployment         Must configure the environment
  Flexible GUI Designer in Visual Studio       Best on CLI operations
  Both good at data structure implementation   
  Compile code                                 Interpreter code
  Closed source                                Open source

Class Diagram
-------------

![](.//media/image3.png){width="5.48in" height="3.4737609361329835in"}

Figure 1 - Model Classes

![](.//media/image4.png){width="5.350968941382328in"
height="4.49563867016623in"}

Figure 2 - Form Classes

Screens
-------

### Main Menu

![](.//media/image5.png){width="6.05292760279965in"
height="3.0837642169728783in"}

Figure 3 - Main Menu Form (APPMENU)

This is screen presents user with the main finalities. Which is,

-   Scan initiation

-   View and compare snapshots

### Interfaces

![](.//media/image6.png){width="6.268055555555556in" height="2.93125in"}

Figure 4 - Interface Selection

Nowadays most of the computers contains more than one network interface
cards. Therefore, during the initial designing I saw a reequipment of
interface information screen before beginning the scan. On this screen
user will be presented with a list of interfaces with this information,

-   NIC Physical Address (MAC Address)

-   Human readable interface name

-   Network ID

-   Subnet Mask

-   NIC manufacture (Vendor) -- this has been identified using an
    inbuilt database of manufacture information corresponding to MAC
    address manufacture specific portion. I have provided this method
    inside the "PHYSICALADDRESSPROCESSOR CLASS".

![](.//media/image7.png){width="3.0720002187226596in"
height="2.671527777777778in"}

Figure 5 - PHYSICALADDRESSPROCESSOR Class

![](.//media/image8.png){width="5.68799978127734in"
height="3.4158912948381452in"}

Figure 6 - PhysicalAddressProcessor.getNICVendor

### Scan

![](.//media/image9.png){width="6.268055555555556in" height="3.49375in"}

Figure 7 - Scan

On Shown event of this screen, scan job will be initiated, and the
progress status is displayed status strip on the bottom of the window.
User has two buttons to perform specific functions.

1.  Scan Start and Cancel

```{=html}
<!-- -->
```
3.  Save to snapshot -- will be disabled after saving snapshot.

![A screenshot of a computer screen Description automatically
generated](.//media/image10.png){width="5.363450349956255in"
height="1.9679997812773404in"}

Figure 8 - Scan Form Constructor

On form initialisation, caller should provide,

-   Network ID

-   Subnet Mask

-   Host Machine IP Address

-   Form caller

With this information, on form's on Shown event Scan methods will be
called on a Background worker class

![](.//media/image11.png){width="5.567361111111111in"
height="7.448862642169729in"}

Figure 9 - Scan Form Scan Function

This function updates the DATAGRIDVIEW as the it identifies active
devices and update a list of active nodes to use on save function if
user chose to save the results.

On save button's Click event, it initializes storage object with active
device on parameters. Storage class is responsible for saving provided
information in file form in a predefined folder

![A screenshot of a computer screen Description automatically
generated](.//media/image12.png){width="6.28in"
height="1.7598097112860893in"}

Figure 10 - Storage Class (Save)

### Snapshot history

![](.//media/image13.png){width="6.268055555555556in"
height="3.6381944444444443in"}

On this screen, users can view the previously obtained/saved scan
results. Users can either view the results by double click on a specific
recode of select 2 results and perform a comparison. Storage class we
used to save snapshots previously is also responsible for obtaining
information for this screen as well.

![A screenshot of a cell phone Description automatically
generated](.//media/image14.png){width="6.095999562554681in"
height="6.306359361329834in"}

Figure 11 - Storage Get Files

![](.//media/image15.png){width="6.268055555555556in"
height="3.4097222222222223in"}

Figure 12 - View single record

![](.//media/image16.png){width="6.268055555555556in"
height="3.7131944444444445in"}

Figure 13 - Compare 2 records

On comparison form, on row click event it shows matching record from the
other file. The perform this action on click event look for similar mac
addresses from two files.

Challengers 
-----------

-   Firewall rule may affect the results of the Software

-   To resolve hostname of each device the program depends on the DNS
    serer of the network. Therefore, availability of hostname is may
    vary on different network. To mitigate this issue, I have included
    NIC manufacture identifier. With it we can get somewhat better idea
    about the device.

-   Most of the devices on power save mode does not replay for ICMP
    (PING) protocol requests. Therefore, program may skip those devices
    even though they are actively connected to the network. Especially
    mobile devices.

Other Similar tools
-------------------

### IP Address Tracker

By far the most powerful tool on the list of free clients, SolarWinds IP
Address Tracker is a standalone solution, available for free download,
that works on its own but is further enhanced by the SolarWinds IPAM
suite when integrated. This makes it an excellent first step if you are
considering a premium option but looking for a fully functional address
tracker in the meantime.

### Angry IP Scanner (Free)

Widely hailed as one of the first and most popular free IP address
scanners, Angry IP Scanner is open-source software, deployable across
operating systems. Windows, Linux, and Mac OS X users will find this
tool handy for its non-existent price tag.

### NETDISCOVER

NETDISCOVER is an active/passive ARP reconnaissance tool, initially
developed to gain information about wireless networks without DHCP
servers in wardriving scenarios. It can also be used on switched
networks. Built on top of LIBNET and LIBPCAP, it can passively detect
online hosts or search for them by sending ARP requests.

Furthermore, it can be used to inspect your network\'s ARP traffic, or
find network addresses using auto scan mode, which will scan for common
local networks.
