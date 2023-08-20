# Core.DataKit
Provide DataWrappers
# Class and Interfaces
1. ```Result<DataType>```:  Class That used to return either data of type ```DataType``` or Exception if the data of type ```DataTyp```e is null it will return ```ArgumentNullException``` offer the following methods
     -  ```ContainError()```: Check if the result is of error type
     -  ```IsErrorType<Type>``` Check if the Exception is Of the type Type
     -  ```IsDataType<Type>``` Check if the Data is Of the type Type
     -  ```IsInternalError``` Check if the Exception is Of the type ```InternalErrorException```
     -  ```GetErrorMessage``` return the Exception message
     -  ```GetError()```: return the error
     -  ```ContainData()```: Check if the result is Data
     -  ```From(DateType?)```: Check if the result is Data
     -  ```Three Implicit``` Conversation from exception and Data To Result, and combine exceptions
2. ```Result```: Offer Static Method To help with using ```Result<DataType>```
     -  ```From<DateType>(DateType?)```: Call ```From(Datatype?)```. help compiler deciding the ```Datatype``` for ```(Datatype?)``` from the ```parameter```
3. ```IDateTimeProvider``` Provide and Interface to mock Datetime
4. ```DateTimeProvider``` Default Implementation for IDateTimeProvider That Use DateTime

5. ```State```:  Class That used to check if operation succed, check if its ok or not ok if not you can get the Exception
     -  ```ContainError()```: Check if the result is of error type
     -  ```IsErrorType<Type>``` Check if the Exception is Of the type Type
     -  ```IsInternalError``` Check if the Exception is Of the type ```InternalErrorException```
     -  ```GetErrorMessage``` return the Exception message
     -  ```GetError()```: return the error
     -  ```IsOk()```: Check if the result of execution is Ok
     -  ```IsNotOk()```: Check if the result of execution is not Ok
     -  ```Three Implicit``` Conversation from exception and Ok To State and combine exceptions
