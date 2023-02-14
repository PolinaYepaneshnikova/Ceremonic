import React, { FC } from 'react';
import './css/button.css'

interface ButtonProps extends React.HTMLAttributes<any> {
    color?: string;
    kind: string;
    fontWeight?: number;
    fontSize?: string;
    lineHeight?: string;
    width?: string;
    borderRadius?: string;
    
}

const Button: FC<ButtonProps> = ({
                                        children,
                                        color,
                                        fontWeight,
                                        lineHeight,
                                        fontSize,
                                        kind,
                                        width,
                                        borderRadius,
                                        ...props
                                     }) => {

    


    let rootClasses = ['button_primary']

    if(kind === 'button_secondary'){
        rootClasses = ['button_secondary']
    }
    if(kind === 'button_with-background'){
        rootClasses = ['button_with-background']
    }

    return (
        <button {...props} 
        style={{color, fontSize, fontWeight, lineHeight, width, borderRadius}} 
        className={rootClasses.join(' ')} >
            {children}
        </button>
    );
};

export default Button;