import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { addTodo } from '../actions/todo';

TodoForm.propTypes = {

};

TodoForm.defaultProp = {

}

function TodoForm(props) {
    const [valueInput, setValueInput] = useState("");

    const dispatch = useDispatch();

    function handleValueChange(e) {
        setValueInput(e.target.value);
    };

    function handleSubmit(e) {
        e.preventDefault();

        const formValues = {
            title: valueInput,
        };

        const actionAdd = addTodo(formValues)
        dispatch(actionAdd);
        setValueInput("");
    }

    return (
        <form onSubmit={handleSubmit}>
            <div className="m-search">
                <div className="m-input-search">
                    <input type="" name=""
                        type="text"
                        placeholder="Add your new todo"
                        value={valueInput}
                        onChange={handleValueChange} />
                </div>
                <button type="submit" className="m-button m-button-search">+</button>
            </div>
        </form>
    );
}

export default TodoForm;