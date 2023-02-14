import React, { FC } from 'react';
import './css/input.css'

interface InputProps extends React.HTMLAttributes<any> {
    kind: string;
    padding?: string;
    borderBottomLeftRadius?: string;
    borderTopLeftRadius?: string;
    
}

const Input: FC<InputProps> = ({
                                        children,
                                        kind,
                                        padding,
                                        borderBottomLeftRadius,
                                        borderTopLeftRadius,
                                        ...props
                                     }) => {

    


    let rootClasses = ['input_primary']

    if(kind === 'input_secondary'){
        rootClasses = ['input_secondary']
    }


    return (
        <input {...props} style={{padding, borderTopLeftRadius, borderBottomLeftRadius}} className={rootClasses.join(' ')} >
            {children}
        </input>
    );
};

export default Input;