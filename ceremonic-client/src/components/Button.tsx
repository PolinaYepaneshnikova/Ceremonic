import React, { FC } from 'react';
import './css/button.css'

interface ButtonProps extends React.HTMLAttributes<any> {
    color?: string;
    big?: boolean;
    small?: boolean;
    kind: string;
    
}

const Button: FC<ButtonProps> = ({
                                        children,
                                        big,
                                        small,
                                        color,
                                        kind,
                                        ...props
                                     }) => {

    


    let rootClasses = ['primary']

    if(kind === 'secondary'){
        rootClasses = ['secondary']
    }


    return (
        <button {...props} style={{color}} className={rootClasses.join(' ')} >
            {children}
        </button>
    );
};

export default Button;