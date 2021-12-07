import React, { useEffect } from 'react';
import PropTypes from 'prop-types';
import TodoItem from './TodoItem';
import { useDispatch, useSelector } from 'react-redux';
import { setTodoList } from '../actions/todo';

TodoList.propTypes = {
    onTodoClick: PropTypes.func,
};

TodoList.defaultProps = {
    onTodoClick: null,
};

function TodoList(props) {
    const { onTodoClick } = props;
    const todoList = useSelector(state => state.todo.list);

    const dispatch = useDispatch();

    useEffect(() => {
        const loca = JSON.parse(localStorage.getItem('todoList'));
        if (loca) {
            const actionSetTodoList = setTodoList(loca);
            dispatch(actionSetTodoList);
        }
    }, []);

    function handleClick(todo) {
        if (onTodoClick) {
            onTodoClick(todo);
        }
    }
    return (
        <div className="m-todo">
            {todoList.map(todo => (
                <div key={todo.id} onClick={() => handleClick(todo)} >
                    <TodoItem
                        todoTitle={todo.title}
                        todoId={todo.id}
                    />
                </div>
            ))}
        </div>
    );
}

export default TodoList;