# AppStoreConnect

This is a dotnet library and cli for interaction with Apples App Store Connect API.
There are great tools like fastlane out there for automating Apple App creation and this tool does not try or can compete with all the features that fastlane offers.

So why did I start this?
There are two reasons:
* Apples 2FA
* Cli

#### Apples 2FA
Apple is moving to 2FA for all Apple developer accounts, unfortunatley although this is great for security it is really bad for automated pipelines. Fastlane is solving this so far by using a second account that does not have a `ACCOUNT_HOLDER` flag and tunred off 2FA.
This is probelematic, if you don't own a Organizational or Enterprise Apple Developer Account and of course turning off 2FA is always a bad idea when it comes to security.
To avoid this, this tool works with Apples App Store Connect-API and Bearer Tokens.

#### Cli
Even though I like what fastlane is doing, I really don't like the pipeline i have to set up for it (Installing ruby, installing fastlane, etc.) And for what I am trying to achieve fastlane does way too much. And there are also the limitations for some features that only work on a Mac.
To be clear though, this tool does not give you as many features as fastlane does!
Afterall I wanted to have a small easy to use command line tool. 

## Install
This app is available as a [dotnet tool](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-tool-install) and as a self contained executeable for Linux, Windows and MacOS

### dotnet tool
To install this app as a dotnet tool run from command line
```bash
dotnet tool install SiaConsulting.AppStoreConnect.Cli --version 0.3 --no-cache --global
```
Update from an older version
```bash
dotnet tool update SiaConsulting.AppStoreConnect.Cli --version 0.3 --no-cache --global
```
### self contained
[Win x64](https://github.com/dersia/AppStoreConnect/releases/download/v0.3/appStoreConnect-win-x64.zip)

[MacOS](https://github.com/dersia/AppStoreConnect/releases/download/v0.3/appStoreConnect-osx-x64.zip)

[Linux](https://github.com/dersia/AppStoreConnect/releases/download/v0.3/appStoreConnect-linux-x64.tar.gz)

## Usage

### Help
if you just run the command you will get the help:
```bash
$ .\appStoreConnect.exe
Required command was not provided.

Usage:
  appStoreConnect [options] [command]

Options:
  -oj, --out-json
  --version          Show version information
  -?, -h, --help     Show help and usage information

Commands:
  bundleIds                          create, update, list BundleIds
  bundleIdCapability                 enable, disable and modify BundleId-Capabilities
  certificates                       create, get, list or revoke certificates
  device, devices                    register, get, list or update devices
  p, profiles                        create, get, list or delete profiles
  userInvitation, userInvitations    invite, get, list or cancel user invitations
  user, users                        update, get, list or delete a user | replace, remove or add apps to a user
  tools                              Tools for certification and jwt handling
```

### Get a Bearer Token
To interact with AppStoreConnect, you will need a valid OAuth Bearer token.
Unfortunatley Apple doesn't provide an Endpoint for accquiring an OAuth token using a client-id and secret.
To get a valid Bearer Token, you will need to create a APIKey and Certificate at [Users and Access](https://appstoreconnect.apple.com/access/api).

You either create a Bearer token yourself or use this app to create one.
```bash
appStoreConnect tools jwt create fromFile <PATH-TO-P8> <KID> <ISSUER ID>
```

### Users-Endpoint
```bash
$ ./appStoreConnect.exe user
Required command was not provided.

users:
  update, get, list or delete a user | replace, remove or add apps to a user

Usage:
  appStoreConnect users [options] [command]

Options:
  -oj, --out-json
  -?, -h, --help     Show help and usage information

Commands:
  u, update <userId> <firstName> <lastName> <roles> <token>       update a user by their id
  uj, ujson, updateFromJson <userId> <token> <json>               update user from user json
  uf, ufile, updateFromFile <userId> <token> <file>               update user from user json file
  g, get <userId> <token>                                         get a user by their id
  l, list <token>                                                 list all users
  d, delete <userId> <token>                                      delete a user by their id
  apps, linkedApps <userId> <token>                               list all apps linked to a user by their id
  appIds, linkedAppIds <userId> <token>                           list all appIds linked to a user by their id
  replaceApps <userId> <apps> <token>                             replace apps for a user by their id
  replaceAppsFromJson, replaceAppsJson <userId> <token> <json>    replace apps for a user from List of Data json
  replaceAppsFile, replaceAppsFromFile <userId> <token> <file>    replace apps for a user from List of Data json file
  removeApps <userId> <apps> <token>                              remove apps for a user by their id
  removeAppsFromJson, removeAppsJson <userId> <token> <json>      remove apps for a user from List of Data json
  removeAppsFile, removeAppsFromFile <userId> <token> <file>      remove apps for a user from List of Data json file
  addApps <userId> <apps> <token>                                 add apps for a user by their id
  addAppsFromJson, addAppsJson <userId> <token> <json>            add apps for a user from List of Data json
  addAppsFile, addAppsFromFile <userId> <token> <file>            add apps for a user from List of Data json file
```

### UserInvitaions
```bash
$ ./appStoreConnect.exe userInvitation
Required command was not provided.

userInvitations:
  invite, get, list or cancel user invitations

Usage:
  appStoreConnect userInvitations [options] [command]

Options:
  -oj, --out-json
  -?, -h, --help     Show help and usage information

Commands:
  i, invite <email> <firstName> <lastName> <token>    invite a user
  ij, ijson, inviteFromJson <token> <json>            invite a new user from userInvitation json
  if, ifile, inviteFromFile <token> <file>            invite a new user from userInvitation json file
  g, get <userInvitationId> <token>                   get a user invitation by its id
  l, list <token>                                     list all userInvitations
  c, cancel <userInvitationId> <token>                cancel a userInvitation by its id
  apps, linkedApps <userInvitationId> <token>         list all apps linked to a userInvitation by its id
  appIds, linkedAppIds <userInvitationId> <token>     list all appIds linked to a userInvitation by its id
```

### BundleId
```bash
$ ./appStoreConnect.exe bundleIds
Required command was not provided.

bundleIds:
  create, update, list BundleIds

Usage:
  appStoreConnect bundleIds [options] [command]

Options:
  -oj, --out-json
  -?, -h, --help     Show help and usage information

Commands:
  l, list <token>
  g, get <bundleIdId> <token>
  r, register <identifier> <name> <IOS|MAC_OS|UNIVERSAL> <token>
  registerFromJson, rj, rjson <token> <json>                              register a new bundleId from bundleId json
  registerFromFile, rf, rfile <token> <file>                              register a new bundleId from bundleId json file
  u, update <id> <token>
  d, delete <bundleIdId> <token>
  capabilities, list-linkedBundleIdCapabilities <bundleIdId> <token>      list all capabilities for given bundleId
  capabilityIds, list-linkedBundleIdCapabilityIds <bundleIdId> <token>    list all capabilityIds for given bundleId
  list-linkedProfiles, profiles <bundleIdId> <token>                      lists all profiles for given bundleId (no-content)
  list-linkedProfileIds, profileIds <bundleIdId> <token>                  list all profileIds for given bundleId
```

### BundleIdCapability
```bash
$ ./appStoreConnect.exe bundleIdCapability
Required command was not provided.

bundleIdCapability:
  enable, disable and modify BundleId-Capabilities

Usage:
  appStoreConnect bundleIdCapability [options] [command]

Options:
  -oj, --out-json
  -?, -h, --help     Show help and usage information

Commands:
  ej, ejson, enableFromJson <token> <json>    enable capability from BundleIdCapability json
  ef, efile, enableFromFile <token> <file>    enable capability from BundleIdCapability json file
  d, disable <capabilityId> <token>
```

### Devices
```bash
$ ./appStoreConnect.exe device
Required command was not provided.

devices:
  register, get, list or update devices

Usage:
  appStoreConnect devices [options] [command]

Options:
  -oj, --out-json
  -?, -h, --help     Show help and usage information

Commands:
  r, register <name> <APPLE_TV|APPLE_WATCH|IPAD|IPHONE|IPOD|MAC> <IOS|MAC_OS|UNIVERSAL> <udid> <token>    register a new device
  registerFromJson, rj, rjson <token> <json>                                                              register a new device from device json
  registerFromFile, rf, rfile <token> <file>                                                              register a new device from device json file
  u, update <deviceId> <token>                                                                            update a device
  uj, ujson, updateFromJson <token> <deviceId> <json>                                                     update a device from device json
  uf, ufile, updateFromFile <token> <deviceId> <file>                                                     update a device from device json file
  g, get <deviceId> <token>                                                                               get a device by its id
  l, list <token>                                                                                         list all devices
```

### Certificates
#### Creating a certificate
To create a certificate you'll need a `Certificate Signing Request (CSR)`. To make it easier to create one you can just use a build-in cert-tool.
```bash
$ ./appStoreConnect.exe tools cert csr <COMMON NAME> <COUNTRY CODE> -ofc ./certificate-request.csr -ofpub ./certificate-public.key -ofpriv ./certificate-private.pem
```
There is also an `interactive`-mode of CSR creation
```bash
$ ./appStoreConnect.exe tools cert csri -ofc ./certificate-request.csr -ofpub ./certificate-public.key -ofpriv ./certificate-private.pem
Creating a Certificate Signing Request
Country Code* [C]: DE
State or Province [ST]:
Locality or City [L]: Dusseldorf
Company [O]: SiaConsulting
Organizational Unit [OU]:
Common Name* [CN]: sia-consulting.eu
Email Address [emailAddress]: info@sia-consulting.eu
```
You can then create a certificate by calling `appStoreConnect.exe certificate createFromFile <BEARER-TOKEN> ./certificate-request.csr --type <CERTIFICATE-TYPE>`
```bash
$ ./appStoreConnect.exe certificates cf
Required argument missing for command: cf

createFromFile:
  create a new certificate from certificate json file

Usage:
  appStoreConnect certificates createFromFile [options] <token> <file>

Arguments:
  <token>
  <file>

Options:
  --type <DEVELOPER_ID_APPLICATION|DEVELOPER_ID_KEXT|DEVELOPMENT|IOS_DEVELOPMENT|IOS_DISTRIBUTION|MAC_APP_DEVELOPMENT|MAC_APP_DISTRIBUTION|MAC_INSTALLER_DISTRIBUTION>
  -oj, --out-json
  -?, -h, --help    Show help and usage information
```
After that you can download the Certificate (CER) from AppStoreConnect
```bash
$ ./appStoreConnect.exe certificates gc <CERTIFIACTE-ID> <BEARER-TOKEN> > ./certificate.cer
```

To sign your app with that Certificate, you need to convert your CER Certificate to a PKCS12 Certificate.
To make it easier you can use a built-in tool to create a PKCS12 certificate.
```bash
$ ./appStoreConnect.exe tools cert p12FromFile ./certificate.cer --private-key ./certificate-private.pem --password <PASSWORD> -of ./certificate.p12
```

Here is the help output of the certifiactes command
```bash
$ ./appStoreConnect.exe certificates
Required command was not provided.

certificates:
  create, get, list or revoke certificates

Usage:
  appStoreConnect certificates [options] [command]

Options:
  -oj, --out-json
  -?, -h, --help     Show help and usage information

Commands:
  c, create <csrContent> <token>              create a new certificate
  cf, cfile, createFromFile <token> <file>    create a new certificate from certificate json file
  g, get <certificateId> <token>              get a certificate by its id
  gc, getContent <certificateId> <token>      get a certificate content by its id
  ge, getEntry <certificateId> <token>        get a certificate without its content by its id
  l, list <token>                             list all certificates (no content)
  r, revoke <certificateId> <token>           revoke a certificate by its id
```

### Profiles
```bash
$ ./appStoreConnect.exe profiles
Required command was not provided.

profiles:
  create, get, list or delete profiles

Usage:
  appStoreConnect profiles [options] [command]

Options:
  -oj, --out-json
  -?, -h, --help     Show help and usage information

Commands:
  c, create <name> <token>                                    create a new profile
  cj, cjson, createFromJson <token> <json>                    create a new profile from profile json
  cf, cfile, createFromFile <token> <file>                    create a new profile from profile json file
  g, get <profileId> <token>                                  get a profile by its id
  gc, getContent <profileId> <token>                          get a profile content by its id
  ge, getEntry <profileId> <token>                            get a profile by its id (without content)
  l, list <token>                                             list all profiles
  d, delete <profileId> <token>                               delete a profile by its id
  bundleId, linkedBundleId <profileId> <token>                get bundleId linked to a profile
  bundleIdId, linkedBundleIdId <profileId> <token>            get bundleIdId linked to a profile
  certificates, linkedCertificates <profileId> <token>        list all certificates linked to a profile
  certificateIds, linkedCertificateIds <profileId> <token>    list all certificateIds linked to a profile
  devices, linkedDevices <profileId> <token>                  list all devices linked to a profile
  deviceIds, linkedDeviceIds <profileId> <token>              list all deviceIds linked to a profile
```

## Future plans
In the future APIs for TestFlight and Reporting-Endpoints will be added.