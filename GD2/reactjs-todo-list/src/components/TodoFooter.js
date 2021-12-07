import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { cleanAllTodo } from '../actions/todo';

TodoFooter.propTypes = {

};

function TodoFooter(props) {
    const todoList = useSelector(state => state.todo.list);
    const dispatch = useDispatch();

    function handleClearAll() {
        const actionClearAll = cleanAllTodo();
        dispatch(actionClearAll);
    }

    return (
        <div className="m-footer">
            <div className="m-footer-title">
                You have {todoList.length} pending tasks
            </div>
            <div className="m-button" onClick={handleClearAll}>
                Clear All
            </div>
        </div>
    );
}

export default TodoFooter;