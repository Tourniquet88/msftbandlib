# MSFTBandLib

Microsoft Band library for C# applications using .NET Standard.

This library contains a reverse-engineering of the Microsoft Band's proprietary Bluetooth protocol to enable new client sync applications to be developed for the Band after official support ends on May 31, 2019.

The sole intention of this project is to provide Microsoft Band owners with a solution to continue using their devices beyond the end of support, with a view to eventually enabling a new community client app to be developed for mobile devices.

MSFTBandLib is currently **experimental** and reliant on native Bluetooth implementations for interfacing with Band devices.

This project has been in large part facilitated by [libmsftband by *ksiazkowicz*](https://github.com/ksiazkowicz/libmsftband), a proof-of-concept reverse engineering of the Band's Bluetooth protocol using Python. This has been instrumental in acting as a reference source when understanding the functioning of the Band's Bluetooth commands.

**Update 22-10-2019** As may be evident from the activity (or rather, the lack thereof) in the repository over the past few months, development of this project has ceased. This is due primarily to the fact that my final Band has suffered its inevitable hardware failure and so my ability to continue development is limited and I find little value in doing so when I no longer have access to a functioning Band. You are welcome to continue developing the project on the foundations I have laid if you still have a Band on your wrist. I do not currently plan to complete any further work in this repository.

©James Walker 2019. Licensed under the MIT License.