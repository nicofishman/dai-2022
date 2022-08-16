import { StatusBar } from 'expo-status-bar';
import { useState } from 'react';
import { StyleSheet, Text, TextInput, TouchableOpacity, View } from 'react-native';

export default function App () {
    const [texto, setTexto] = useState('');
    const [textoMostrar, setTextoMostrar] = useState('');

    return (
        <View style={styles.container}>
            <View style={styles.box}>
                <TextInput placeholder="Que onda bro" style={styles.input} onChangeText={(e) => setTexto(e)} />
                <TouchableOpacity style={styles.button} onPress={() => setTextoMostrar(texto)}>
                    <Text style={{ color: 'white', textTransform: 'uppercase' }}>Presioname bb</Text>
                </TouchableOpacity>
                <Text style={styles.counter}>Caracteres: {texto.length}</Text>
                <Text style={{ color: 'white', fontWeight: '700' }}>{textoMostrar}</Text>
            </View>
            <StatusBar style="auto" />
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#fff',
        alignItems: 'center',
        justifyContent: 'center'
    },
    counter: {
        color: 'white',
        fontWeight: '700',
        fontSize: 20,
        width: '100%',
        paddingHorizontal: 30,
        textAlign: 'right'
    },
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
