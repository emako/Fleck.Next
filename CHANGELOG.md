### 1.2.0

- Originally forked from [statianzo/Fleck](https://github.com/statianzo/Fleck)

### 1.2.1

- Add `SetKeepAlive` to socket APIs
- Add more TFMs and package with symbol

### 1.2.2

- Make `retryCount` of `SetKeepAlive` optional (default 5)
- Modernize C# syntax and clean up code

### 1.2.3

- Fix the issue no not log the information [#348](https://github.com/statianzo/Fleck/pull/348/changes)

- Catch exceptions when setting socket keep-alive (`Exception thrown: 'System.ObjectDisposedException' in System.dll`)
