import React, {useState} from "react";

type CreateTeamDto = {
    name: string
}

export default function CreateTeamForm(){

    const [createDto, setCreateDto] = useState<CreateTeamDto>({
        name: ''
    })

    function handleChange() {
        // TODO
    }

    function handleSubmit() {
        // TODO
    }

    return (<div className='form'>
        <h1>Create team</h1>

        <div className='input-group'>
            <label>Team name</label>
            <input className='input-field' type="password" name='password' value={createDto.name}
                   onChange={handleChange}/>
        </div>
        <br/>

        <button className='button' onClick={handleSubmit}>
            Create Team
        </button>
    </div>)
}