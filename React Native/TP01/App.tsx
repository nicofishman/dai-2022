import { StatusBar } from 'expo-status-bar';
import { useState } from 'react';
import { StyleSheet, Text, TextInput, TouchableOpacity, View } from 'react-native';
import { TailwindProvider, useTailwind } from 'tailwind-rn';

import utilities from './tailwind.json';

export default function App () {
    const [texto, setTexto] = useState('');
    const [textoMostrar, setTextoMostrar] = useState('');

    const tailwind = useTailwind();

    return (
        <TailwindProvider utilities={utilities}>
            <View style={tailwind('bg-black w-full h-screen justify-center items-center')}>
                <View style={tailwind('w-12 items-center justify-center p-10 bg-red-300')}>
                    <TextInput placeholder="Que onda bro" style={styles.input} onChangeText={(e) => setTexto(e)} />
                    <TouchableOpacity style={styles.button} onPress={() => setTextoMostrar(texto)}>
                        <Text style={{ color: 'white', textTransform: 'uppercase' }}>Presioname bb</Text>
                    </TouchableOpacity>
                    <Text>{textoMostrar}</Text>
                </View>
                <StatusBar style="auto" />
            </View>
        </TailwindProvider>
    );
}

const styles = StyleSheet.create({
    input: {
        borderWidth: 1,
        borderColor: '#000',
        backgroundColor: '#fff',
        padding: 10,
        margin: 10,
        width: '80%'
    },
    button: {
        backgroundColor: '#2196f3',
        padding: 10,
        margin: 10,
        width: '80%',
        justifyContent: 'center',
        alignItems: 'center'
    },
    box: {
        width: '90%',
        alignItems: 'center',
        justifyContent: 'center',
        padding: 10,
        borderWidth: 1,
        backgroundColor: '#f0ff'
    }

});
