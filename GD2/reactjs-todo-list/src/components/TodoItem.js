import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { useDispatch } from 'react-redux';
import { delTodo } from '../actions/todo';

TodoItem.propTypes = {
    todoTitle: PropTypes.string,
    todoId: PropTypes.string,
};
TodoItem.defaultProps = {
    todoTitle: null,
    todoId: null,
}

function TodoItem(props) {
    const { todoTitle, todoId } = props;
    const [isShowBtnDel, setIsShowBtnDel] = useState(false);
    const dispatch = useDispatch();

    function onClickBtn(id) {
        // dispatch action to delete
        const actionDel = delTodo(id);
        dispatch(actionDel);
    }

    let divBtnDel = () => {
        return (
            <div className="m-todo-item-delete"
                onClick={() => onClickBtn(todoId)}
            >
                Del
            </div>);
    }

    return (
        <div
            key={todoId}
            onMouseEnter={() => { setIsShowBtnDel(true) }}
            onMouseLeave={() => { setIsShowBtnDel(false) }}>
            <div className="m-todo-item">
                <div className="m-todo-item-title">
                    {todoTitle}
                </div>
                {
                    isShowBtnDel ? divBtnDel() : null
                }
            </div>
        </div>

    );
}

export default TodoItem;