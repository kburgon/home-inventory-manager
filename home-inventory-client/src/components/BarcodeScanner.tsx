import { BrowserMultiFormatReader, DecodeHintType, Result } from '@zxing/library';
import { useEffect, useMemo, useRef } from 'react';

interface BarcodeScannerOptions {
	hints?: Map<DecodeHintType, any>;
	constraints?: MediaStreamConstraints;
	timeBetweenDecodingAttempts?: number;
	onResult?: (result: Result) => void;
	onError?: (error: Error) => void;
}

interface BarcodeScannerProperties {
	onResult?: (result: Result) => void;
	onError?: (error: Error) => void;
}

const useBarcodeScanner = ({
	constraints = {
		audio: false,
		video: {
			facingMode: 'environment',
		},
	},
	hints,
	timeBetweenDecodingAttempts = 300,
	onResult = () => {},
	onError = () =>	{}
}: BarcodeScannerOptions = {}) => {
	const ref = useRef<HTMLVideoElement>(null);

	const reader = useMemo<BrowserMultiFormatReader>(() => {
		const instance = new BrowserMultiFormatReader(hints);
		instance.timeBetweenDecodingAttempts = timeBetweenDecodingAttempts;
		return instance;
	}, [hints, timeBetweenDecodingAttempts]);

	useEffect(() => {
		if (!ref.current) return;
		reader.decodeFromConstraints(constraints, ref.current, (result, error) => {
			if (result) onResult(result);
			if (error) onError(error);
		});
		return () => {
			reader.reset();
		};
	}, [ref, reader]);

	return { ref };
};

export const BarcodeScanner: React.FC<BarcodeScannerProperties> = ({
	onResult = () => {},
	onError = () => {},
}) => {
	const { ref } = useBarcodeScanner({ onResult, onError });
	return <video ref={ref} />;
};
