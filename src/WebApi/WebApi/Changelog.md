# v7.1.0 (20.07.2019) [Pull Request](https://github.com/oskardudycz/GoldenEye/pull/71)

## Changes

* Updated `Shared.Core` to `6.1.0` **[MINOR]**
* Updated `Shared.Core.Validation` to `6.1.0` **[MINOR]**
* Updated `Backend.Core` to `7.1.0` **[MINOR]**

# v7.0.0 (26.01.2019) [Pull Request](https://github.com/oskardudycz/GoldenEye/pull/69)

## Changes

* Updated reference to `Shared.Core` **[MAJOR]**
* Updated reference to `Backend.Core` **[MAJOR]**
* Updated `Microsoft.AspNetCore.Mvc` to `2.2.0` **[MINOR]**
* Updated `Microsoft.AspNetCore.Server.Kestrel` to `2.2.0` **[MINOR]**
* Updated `Microsoft.AspNetCore.Server.Kestrel.Https` to `2.2.0` **[MINOR]**
* Updated `Swashbuckle.AspNetCore` to `4.0.1` **[MINOR]**

# v6.1.0 (23.06.2019) [Pull Request](https://github.com/oskardudycz/GoldenEye/pull/65)

## Changes

* Updated reference to `Shared.Core` **[MINOR]**
* Updated reference to `Backend.Core` **[MINOR]**

# v6.0.0 (19.06.2019) [Pull Request](https://github.com/oskardudycz/GoldenEye/pull/64)

## Changes

* Updated reference to Shared.Core **[MAJOR]**

# v5.2.0 (21.05.2019) [Pull Request](https://github.com/oskardudycz/GoldenEye/pull/59)

## Changes

* Updated reference to Backend.Core **[MINOR]**

# v5.0.1 (12.05.2019) [Pull Request](https://github.com/oskardudycz/GoldenEye/pull/61)

## Changes

* Updated reference to Backend.Core **[PATCH]**


# v5.0.0 (12.05.2019) [Pull Request](https://github.com/oskardudycz/GoldenEye/pull/60)

## Changes

* Updated reference to Backend.Core **[MAJOR]**

# v4.0.0 (19.04.2019) [Pull Request](https://github.com/oskardudycz/GoldenEye/pull/58)

## Changes

* Renamed Module Base classes to don't have `Base` suffix **[MAJOR]**
* Removed IConfiguration from base classes constructors **[MAJOR]**
* Moved modules creation to DI **[MAJOR]**
* Added registration helpers for configuring and Using modules **[MINOR]**
* Renamed OnStartup method of module to Use to be aligned with DI conventions **[MAJOR]**
* Added registration of configuration **[MAJOR]**

# v3.0.9 (18.04.2019) [Pull Request](https://github.com/oskardudycz/GoldenEye/pull/57)

## Changes

* updated packages version to most recent **[PATCH]**

# v3.0.8 (08.04.2019) [Pull Request](https://github.com/oskardudycz/GoldenEye/pull/54)

## Changes

* Added package icon **[PATCH]**


# v3.0.7 (07.04.2019) [Pull Request](https://github.com/oskardudycz/GoldenEye/pull/53)

## Changes

* updated packages version to most recent **[PATCH]**


# v3.0.0 (29.12.2017) [Pull Request](https://github.com/oskardudycz/GoldenEye/pull/44)

## Changes

* updated packages version to most recent, breaking changes after migration to MediatR 4 **[MAJOR]**
* added proper handling of `CancellationToken` for async methods in Controllers base classes to be aligned with other async handling conventions **[MAJOR]**

# v2.3.0 (28.12.2017)

## Changes

* added [ExceptionHandlingMiddleware](Exceptions/ExceptionHandlingMiddleware.cs) and [ExceptionToHttpStatusMapper](Exceptions/ExceptionToHttpStatusMapper.cs) for mapping exception to proper HttpStatus **[MAJOR]**

# v2.0.0

## Changes

* Refactored various interfaces, brought final, production ready version of classes **[MAJOR]**

# v1.0.0

## Changes

* Initial set of interfaces and base classes **[MAJOR]**
