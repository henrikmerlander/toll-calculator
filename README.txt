# Improvements
* Unit tests added to verify each requirement.
* Business logic regarding holidays and the fee schedule are handled in their own classes.
* Vehicle enum removed, adding new vehicles simply means implementing the IVehicle interface.
* General refactoring and simplifications, backed by unit tests.

# Bugfixes
* The maximum fee per day did not work when passing over an hour after the first pass.
* Passing multiple times in one hour several times during a day did not charge correctly.
* The existing fee schedule had some peculiar intervals.
* A NuGet package is used for holidays instead of hard coding holidays on a yearly basis.
