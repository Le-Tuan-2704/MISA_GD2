import { useEffect, useState } from 'react';
import './App.css';
import TodoFooter from './components/TodoFooter';
import TodoForm from './components/TodoForm';
import TodoList from './components/TodoList';

function App() {

  function handleTodoClick(todo) {
    console.log(todo);
  }

  return (
    <div className="app">
      <div className="m-box">
        <div>
          <h1>
            Todo App
          </h1>
          <div>
            <TodoForm />
          </div>
          <div>
            <TodoList onTodoClick={handleTodoClick} />
          </div>

        </div>
        <div>
          <TodoFooter />
        </div>
      </div >

    </div>
  );
}

export default App;
