# Core.DataKit
Provide DataWrappers
# Class and Interfaces
1. ```Result<DataType>```:  Class That used to return either data of type DataType or Exception if the data of type DataType is null it will return ArgumentNullException offer the following methods
     -  ```ContainError()```: Check if the result is of error type
     -  ```IsErrorType<Type>``` Check if the Exception is Of the type Type
     -  ```IsDataType<Type>``` Check if the Data is Of the type Type
     -  ```IsInternalError``` Check if the Exception is Of the type InternalErrorException
     -  ```GetErrorMessage``` return the Exception message
     -  ```GetError()```: return the error
     -  ```ContainData()```: Check if the result is Data
     -  ```Two Implicit``` Conversation from exception and Data To Result
2. ```IDateTimeProvider``` Provide and Interface to mock Datetime
3. ```DateTimeProvider``` Default Implementation for IDateTimeProvider That Use DateTime
