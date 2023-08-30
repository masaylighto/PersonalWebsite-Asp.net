# Core.LinqExtensions
Provide Linq Extensions
# Class and Interfaces
1. ```IEnumerableExtension```:  Class That Provide Linq Extensions
     -  ```Apply<T>(Action<T>)```: Apply Method on The Data Work like select but without returning data, it receive an action and preform it on data
     -  ```Page<T>(int pageSize, int pageNumber = 0)``` : Apply Pagination On data, receive two paramter to specifiy the page
2. ```IQueryableExtension```:  Class That Provide Linq Extensions
     -  ```Select<TSource, TResult>(Func<TSource, TResult>)```: Select Fields from Entity and return them as IAsyncEnumerable,receive Function that specify the selected fields
     -  ```SelectAsync<TSource, TResult>(Func<TSource, ValueTask<TResult>>)```: Select Fields from Entity and return them as IAsyncEnumerable, receive Function that specify the selected fields
     -  ```Page(int pageSize, int pageNumber = 0)```: Apply Pagination On data, receive two paramter to specifiy the page



